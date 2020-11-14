using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Feature.RecipientAddress.Commands.CreateByFile;
using Application.Feature.RecipientAddress.Commands.CreateRecipientAddress;
using Application.Feature.RecipientAddress.Commands.DeleteRecipientAddress;
using Application.Feature.RecipientAddress.Commands.UpdateRecipientAddress;
using Application.Feature.RecipientAddress.Queries.GetRecipientAddress;
using Application.Feature.UsersAddresses.Queries;
using DevExtreme.AspNet.Data;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace WebUI.Controllers
{
    public class RecipientAddressController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            loadOptions.RequireTotalCount = true;
            return Ok(await DataSourceLoader.LoadAsync(await Mediator.Send(new GetUsersAddresses()), loadOptions));
        }

        [HttpPost("scope")]
        public async Task<ActionResult> Upload()
        {
            try
            {
                // var path = Path.Combine(@"C:\Users\korzhov.a\Desktop\WebApplication\WebApplication\WebUI\wwwroot", "uploads");
                // Uncomment to save the file
                // if(!Directory.Exists(path))
                // Directory.CreateDirectory(path);

                var files = Request.Form.Files.GetFiles("files[]");
                if (!files.Any()) Response.StatusCode = 400;


                foreach (var file in files)
                {
                    //application/vnd.ms-excel ----- CSV
                    //application/vnd.openxmlformats-officedocument.spreadsheetml.sheet --- XL

                    if (file.FileName.EndsWith(".csv"))
                    {
                        using TextReader sr = new StringReader(await file.ReadAsStringAsync());
                        await Mediator.Send(new CreateByFileCommand
                        {
                            TextReader = sr,
                            FileType = FileTypeEnum.Csv
                        });
                        // await using var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName));
                        // await file.CopyToAsync(fileStream);
                    }
                    else
                    {
                        using var excelPackage = new ExcelPackage(file.OpenReadStream());
                        await Mediator.Send(new CreateByFileCommand
                        {
                            ExcelPackage = excelPackage,
                            FileType = FileTypeEnum.Excel
                        });
                    }
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipientAddressCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateRecipientAddressCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRecipientAddressCommand {Id = id});

            return NoContent();
        }
    }
}