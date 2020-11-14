<template>
  <div>
    <h2 id="tooltip-paragraph-name" class="content-block text--ellipsis">Список адресатов  (всего:{{ totalCount }})</h2>
    <DxDataGrid
        class="dx-card wide-card cut-dx-component"
        :ref="gridRefName"
        :data-source="dataSourceAddresses"
        :focused-row-index="0"
        :show-borders="false"
        :focused-row-enabled="true"
        :column-auto-width="true"
        :column-hiding-enabled="true"
        :word-wrap-enabled="true"
        :row-alternation-enabled="false"
        :allow-column-reordering="true"
        :remote-operations="true"

        :repaint-changes-only="true"
        :highlight-changes="true"

        :allow-column-resizing="true"

        @toolbar-preparing="onToolbarPreparing($event)"
        @exporting="onExporting"
        
        @content-ready="contentReady"
    >
      <DxColumn
          :width="90"
          :hiding-priority="6"
          data-field="id"
          :allow-editing="false"
          :visible="false"
      />
      <DxColumn
          caption="Исходный адрес"
          :hiding-priority="7"
          data-field="inputAddress"
      />
      <DxColumn caption="Полученный адрес">
        <DxColumn
            caption="Индекс"
            :hiding-priority="8"
            data-field="index"
        />
        <DxColumn
            caption="Полный адрес"
            :hiding-priority="9"
            data-field="outputAddress"
        />
      </DxColumn>
      <DxColumn
          caption="Результат анализа">
        <DxColumn
            caption="Статус"
            :hiding-priority="10"
            data-field="state"
            data-type="string"
        >
          <DxLookup :data-source="codeState" display-expr="name" value-expr="code"/>
        </DxColumn>
        <DxColumn
            caption="Результат проверки"
            :hiding-priority="11"
            data-field="accuracy"
        >
        <DxLookup :data-source="accuracy" display-expr="name" value-expr="code"/>
      </DxColumn>
        <DxColumn
            caption="Тип адреса"
            :hiding-priority="9"
            data-field="addrType"
            :visible="false"
        >
          <DxLookup :data-source="addrType" display-expr="name" value-expr="code"/>
        </DxColumn>
        <DxColumn
            caption="Доставочность адреса"
            :hiding-priority="10"
            data-field="delivery"
            :visible="false"
        >
          <DxLookup :data-source="delivery" display-expr="name" value-expr="code"/>
        </DxColumn>
        <DxColumn
            caption="Классификатор отправления"
            :hiding-priority="11"
            data-field="direct"
            :visible="false"
        >
          <DxLookup :data-source="direct" display-expr="name" value-expr="code"/>
        </DxColumn>
        <DxColumn
            caption="Источник данных"
            :hiding-priority="12"
            data-field="origin"
            :visible="false"
        >
          <DxLookup :data-source="origin" display-expr="shortName" value-expr="code"/>
        </DxColumn>
      </DxColumn>

      <DxColumn
          caption="Внесший данные пользователь"
          :hiding-priority="7"
          data-field="userId"
      >
        <DxLookup :data-source="dataSourceUsers" display-expr="userName" value-expr="id"/>
      </DxColumn>
      <DxColumn
          caption="Дата внесения данных"
          :hiding-priority="7"
          data-field="created"
          data-type="datetime"
      />
      <DxColumn
          caption="Управление"
          cell-template="buttonBlockCellTemplate"
          :width="90"
          :visible="false"
      />
      <DxStateStoring
          :enabled="true"
          type="localStorage"
          storage-key="App_AdressesGridStorage"
      />
      <DxSummary>
        <DxTotalItem
            column="inputAddress"
            summary-type="count"
            display-format="Всего записей: {0}"
        />
      </DxSummary>
      <DxSorting/>
      <DxPaging :page-size="50"/>
      <DxFilterRow :visible="true"/>
      <DxLoadPanel :enabled="false"/>
      <DxSearchPanel :visible="true"/>
      <DxGroupPanel :visible="true" emptyPanelText=""/>
      <DxSortByGroupSummaryInfo summary-item="count"/>
      <DxGrouping :auto-expand-all="false"/>
      <DxHeaderFilter :visible="true" :allow-search="true"/>
      <DxSelection mode="multiple"/>
      <DxColumnChooser :enabled="true" mode="select"/>
      <DxScrolling
          mode="virtual"
          row-rendering-mode="standard"
      />

      <DxExport
          :enabled="true"
          :allow-export-selected-data="true"
      />

      <template #buttonBlockCellTemplate={data}>
        <div class="dx-command-edit dx-command-edit-with-icons">
          <a
              href="#"
              class="dx-link dx-icon-edit dx-link-icon"
              title="Изменить"
              v-on:click="onUpdateClick(data.data)"></a>
          <a
              href="#"
              class="dx-link dx-icon-trash dx-link-icon"
              title="Удалить"
              v-on:click="onDeleteClick(data.data)"></a>
        </div>
      </template>
    </DxDataGrid>

    <DxTooltip
        :position="'top'"
        :target="`#tooltip-paragraph-name`"
        hideEvent="dxhoverend"
        showEvent="dxhoverstart"
    >
      <div class="block-tooltip-item">Список адресатов</div>
    </DxTooltip>
    <MultiAddressesForm
        :visible.sync="formVisible"
        :form-data.sync="formData"
        @submit="onFormSubmit"
    />
  </div>
</template>

<script>
import {DxTooltip} from 'devextreme-vue';

import DxDataGrid, {
  DxColumn,
  DxFilterRow,
  DxPaging,
  DxSearchPanel,
  DxColumnChooser,
  DxSorting,
  DxHeaderFilter,
  DxStateStoring,
  DxScrolling,
  DxLoadPanel,
  DxGroupPanel,
  DxGrouping,
  DxSummary,
  DxGroupItem,
  DxLookup,
  DxExport,
  DxSelection,
  DxSortByGroupSummaryInfo,
  DxTotalItem,
} from "devextreme-vue/data-grid";

import origin from "../origin";
import direct from "../direct";
import delivery from "../delivery"
import codeState from "../code-state";
import addrType from "../type-address";
import accuracy from "../accuracy"
import axios from 'axios';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import {confirm} from "devextreme/ui/dialog";
import ExcelJS from 'exceljs';
import {getNotifyErrorMessage} from "../utils/validation"
import MultiAddressesForm from "../components/MultiAddressesForm";
import {exportDataGrid} from 'devextreme/excel_exporter';
import saveAs from 'file-saver';

const dataSourceAddresses = AspNetData.createStore({
  key: 'id',
  loadUrl: `/api/recipientaddress`,
  onBeforeSend: (method, ajaxOptions) => {
    ajaxOptions.xhrFields = {withCredentials: true};
  },
});

const dataSourceUsers = AspNetData.createStore({
  key: 'id',
  loadUrl: `/api/user/all-users`,
  onBeforeSend: (method, ajaxOptions) => {
    ajaxOptions.xhrFields = {withCredentials: true};
  },
});

export default {
  name: "addresses",
  data() {
    return {
      totalCount:0,
      direct,
      codeState,
      addrType,
      delivery,
      origin,
      accuracy,
      dataSourceAddresses,
      dataSourceUsers,
      gridRefName: 'dataGrid',
      formData: {},
      formVisible: false,
    };
  },
  created() {

  },
  methods: {
    contentReady(){
      this.totalCount =  this.$refs[this.gridRefName].instance.totalCount();
    },
    onExporting(e) {
      const workbook = new ExcelJS.Workbook();
      const worksheet = workbook.addWorksheet('P2W');

      exportDataGrid({
        component: e.component,
        worksheet: worksheet,
        autoFilterEnabled: true
      }).then(() => {
        // https://github.com/exceljs/exceljs#writing-xlsx
        workbook.xlsx.writeBuffer().then((buffer) => {
          saveAs(new Blob([buffer], {type: 'application/octet-stream'}), 'P2W_Export.xlsx');
        });
      });
      e.cancel = true;
    },
    onFormSubmit(e, data) {
      this.formData = {};
      this.formVisible = false
    },
    onInsertClick() {
      this.formData = {};
      this.formVisible = true
    },
    onUpdateClick(data) {
      this.formData = data;
      this.formVisible = true
    },
    onDeleteClick(data) {
      let message = confirm("Вы уверены, что хотите удалить эту запись?", "Удаление");
      message.then((dialogResult) => {
        if (dialogResult) {
          axios.delete(`/api/recipientaddress/${data.id}`)
              .then(() => {
                this.refreshDataGrid();
              })
              .catch(reason => {
                getNotifyErrorMessage(reason);
              });
        }
      });
    },
    onToolbarPreparing(e) {
      e.toolbarOptions.items.unshift(
          {
            location: 'after',
            widget: 'dxButton',
            options: {
              hint: 'Добавить',
              icon: 'plus',
              type: 'normal',
              focusStateEnabled: false,
              onClick: this.onInsertClick

            }
          },
          {
            location: 'after',
            widget: 'dxButton',
            options: {
              icon: 'refresh',
              onClick: this.refreshDataGrid.bind(this)
            }
          });
    },
    refreshDataGrid() {
      this.$refs[this.gridRefName].instance.refresh();
    },
  },
  components: {
    MultiAddressesForm,
    DxDataGrid,
    DxColumn,
    DxFilterRow,
    DxPaging,
    DxSearchPanel,
    DxColumnChooser,
    DxSorting,
    DxHeaderFilter,
    DxStateStoring,
    DxScrolling,
    DxLoadPanel,
    DxGroupPanel,
    DxGrouping,
    DxSummary,
    DxGroupItem,
    DxLookup,
    DxExport,
    DxSelection,
    DxSortByGroupSummaryInfo,
    DxTotalItem,
    DxTooltip,
  }
};
</script>
