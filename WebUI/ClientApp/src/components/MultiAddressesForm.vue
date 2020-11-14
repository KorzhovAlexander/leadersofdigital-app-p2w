<template>
  <DxPopup
      height="auto"
      :width="728"
      position="center"
      title="Форма добавления адресатов"
      :show-title="true"
      :resize-enabled="true"
      :visible="visible"
      :close-on-outside-click="true"
      @hidden="cancel"
  >
    <div>
      <form
          id="form"
          :ref="formRefName"
          method="post"
          action=""
          enctype="multipart/form-data"
      >
        <div class="fileuploader-container">
          <div
              id="dropzone-external"
              class="flex-box"
              :class="[isDropZoneActive ? 'dx-theme-accent-as-border-color dropzone-active' : 'dx-theme-border-color']"
          >
            <div v-if="done">OK</div>
            <div
                id="dropzone-text"
                class="flex-box"
            >
              <span>Перенесите свои файлы в поле</span>
              <span>…или нажмите сюда и выберете нужные файлы.</span>
            </div>
            <DxProgressBar
                id="upload-progress"
                :min="0"
                :max="100"
                width="30%"
                :show-status="false"
                :visible="progressVisible"
                :value="progressValue"
            />
          </div>
          <DxFileUploader
              id="file-uploader"
              select-button-text="Select .csv .xl"
              dialog-trigger="#dropzone-external"
              drop-zone="#dropzone-external"
              :multiple="true"
              :allowed-file-extensions="allowedFileExtensions"
              name="files[]"
              upload-mode="instantly"
              upload-url="api/recipientaddress/scope"
              :visible="false"
              @before-send="submit"
              @drop-zone-enter="dropZoneEnter"
              @drop-zone-leave="dropZoneLeave"
              @uploaded="uploaded"
              @progress="progress"
              @upload-started="uploadStarted"
          />
        </div>
        <DxButton
            style="margin-top: 20px"
            class="button"
            text="Отменить"
            icon="close"
            type="danger"
            styling="outline"
            @click="cancel"
            v-on:keyup.esc="cancel"
        />
      </form>
    </div>
  </DxPopup>
</template>

<script>
import {DxPopup} from 'devextreme-vue/popup';
import {DxFileUploader} from 'devextreme-vue/file-uploader';
import {DxTextBox} from 'devextreme-vue/text-box';
import {DxButton} from 'devextreme-vue/button';
import notify from 'devextreme/ui/notify';
import {DxProgressBar} from 'devextreme-vue/progress-bar';

export default {
  name: "MultiAddressesForm",
  props: {
    visible: {
      type: Boolean,
      required: true
    },
    formData: {
      type: Object,
      required: true
    },
  },
  computed: {
    form: function () {
      return this.$refs[this.formRefName].instance;
    },
  },

  data: function () {
    return {
      popupVisible: false,
      formRefName: 'form',

      isDropZoneActive: false,
      done: false,
      progressVisible: false,
      progressValue: 0,
      allowedFileExtensions: ['.csv', '.XLSX', '.XLSM', '.XLSB', '.XLTX', '.XLTM', '.XLT', '.XLS', '.XML', '.XLAM', '.XLA', '.XLW']

    }
  },
  methods: {
    cancel: function () {
      this.$emit('update:visible', false);
    },

    dropZoneEnter(e) {
      if (e.dropZoneElement.id === 'dropzone-external') {
        this.isDropZoneActive = true;
      }
    },
    dropZoneLeave(e) {
      if (e.dropZoneElement.id === 'dropzone-external') {
        this.isDropZoneActive = false;
      }
    },

    uploaded(e) {
      const file = e.file;
      const dropZoneText = document.getElementById('dropzone-text');
      const fileReader = new FileReader();
      fileReader.onload = () => {
        this.isDropZoneActive = false;
        this.done = true;
        notify('Адресаты успешно загружены', 'success');
      };
      fileReader.readAsDataURL(file);
      dropZoneText.style.display = 'none';
      this.progressVisible = false;
      this.progressValue = 0;
    },
    progress(e) {
      this.progressValue = e.bytesLoaded / e.bytesTotal * 100;
    },
    uploadStarted() {
      this.done = false;
      this.progressVisible = true;
    },


    submit: function (e) {
      notify('Отправка началась...');
      // this.$emit('submit', this.formData);
    },
  },
  components: {
    DxPopup,
    DxFileUploader,
    DxTextBox,
    DxButton,
    DxProgressBar,
  },
}
</script>

<style>
#dropzone-external {
  /*width: 350px;*/
  height: 350px;
  background-color: rgba(183, 183, 183, 0.1);
  border-width: 2px;
  border-style: dashed;
  padding: 10px;
}

#dropzone-external > * {
  pointer-events: none;
}

#dropzone-external.dropzone-active {
  border-style: solid;
}

.widget-container > span {
  font-size: 22px;
  font-weight: bold;
  margin-bottom: 16px;
}

#dropzone-image {
  max-width: 100%;
  max-height: 100%;
}

#dropzone-text > span {
  font-weight: 100;
  opacity: 0.5;
}

#upload-progress {
  display: flex;
  margin-top: 10px;
}

.flex-box {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
</style>  