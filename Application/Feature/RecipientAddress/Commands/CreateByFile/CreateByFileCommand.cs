using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Data.Persistence;
using CsvHelper;
using Hangfire;
using MediatR;
using OfficeOpenXml;

namespace Application.Feature.RecipientAddress.Commands.CreateByFile
{
    public class CreateByFileCommand : IRequest
    {
        public ExcelPackage ExcelPackage { get; set; }
        public TextReader TextReader { get; set; }
        public FileTypeEnum FileType { get; set; }
    }

    public enum FileTypeEnum
    {
        Csv = 1,
        Excel = 2,
    }


    public class CreateByFileCommandHandler : IRequestHandler<CreateByFileCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly RecordAnaliseJob _recordAnaliseJob;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public CreateByFileCommandHandler(ApplicationDbContext context, RecordAnaliseJob recordAnaliseJob,
            ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _recordAnaliseJob = recordAnaliseJob;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        private const int Pools = 250;

        public Task<Unit> Handle(CreateByFileCommand request, CancellationToken cancellationToken)
        {
            //todo парсинг с файлов и отрпвка 100500 запросов на api
            var requestList = new List<ApiRequest>();
            if (request.FileType == FileTypeEnum.Csv)
            {
                using var csv = new CsvReader(request.TextReader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<RecordCsv>();

                foreach (var record in records)
                {
                    if (record.Address.Length < 10)
                    {
                        //todo невалидный адресс
                    }
                    //todo и если не нашел регион


                    requestList.Add(new ApiRequest {addr = new List<AddrDto> {new AddrDto {val = record.Address}}});
                }
                var requestListCount = requestList.Count / Pools + 1;
                Console.WriteLine(requestListCount);
                for (var i = 0; i < requestListCount; i++)
                {
                    var i1 = i;
                    BackgroundJob.Enqueue(() => _recordAnaliseJob
                        .GenerateRows(requestList
                            .Skip(i1 * Pools)
                            .Take(Pools), _currentUserService.UserId));
                }
            }

            if (request.FileType == FileTypeEnum.Excel)
            {
                var worksheet = request.ExcelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null) return Task.FromResult(Unit.Value);

                var cellsText = worksheet.Cells
                    .Where(cell => cell.Start.Column == 1)
                    .Select(cell => cell.Text).ToList();

                foreach (var text in cellsText)
                {
                    if (text.Length < 10)
                    {
                        //todo невалидный адресс
                    }
                    //todo и если не нашел регион

                    requestList.Add(new ApiRequest {addr = new List<AddrDto> {new AddrDto {val = text}}});
                }

                var requestListCount = requestList.Count / Pools + 1;
                Console.WriteLine(requestListCount);
                for (var i = 0; i < requestListCount; i++)
                {
                    var i1 = i;
                    BackgroundJob.Enqueue(() => _recordAnaliseJob
                        .GenerateRows(requestList
                            .Skip(i1 * Pools)
                            .Take(Pools), _currentUserService.UserId));
                }

                // BackgroundJob.Enqueue(() => _recordAnaliseJob.GenerateRows(requestList, _currentUserService.UserId));
            }

            return Task.FromResult(Unit.Value);
        }
    }
}