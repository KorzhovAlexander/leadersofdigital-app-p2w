<template>
  <div class="user-panel">
    <div class="user-info">
      <div class="image-container">
        <div class="user-image" />
      </div>
      <div class="user-name">{{ currentUser.userName }}</div>
    </div>

    <DxContextMenu
      v-if="menuMode === 'context'"
      target=".user-button"
      :items="menuItems"
      :width="210"
      show-event="dxclick"
      css-class="user-menu"
    >
      <DxPosition my="top center" at="bottom center" />
    </DxContextMenu>

    <DxList
        v-if="menuMode === 'list'"
        class="dx-toolbar-menu-action"
        :items="menuItems"
    />
  </div>
</template>

<script>
import DxContextMenu, { DxPosition } from "devextreme-vue/context-menu";
import DxList from "devextreme-vue/list";
import {mapState} from 'vuex';

export default {
  props: {
    menuMode: String,
    menuItems: Array,
    user: Object
  },
  components: {
    DxContextMenu,
    DxPosition,
    DxList
  },
  computed: {
    ...mapState(['currentUser',]),
  },
};
</script>

<style lang="scss">

.user-info {
  display: flex;
  align-items: center;
  height: 30px;

  .dx-toolbar-menu-section & {
    padding: 10px 6px;
    border-bottom: 1px solid rgba(0, 0, 0, 0.1);
  }

  .image-container {
    overflow: hidden;
    border-radius: 50%;
    height: 30px;
    width: 30px;
    margin: 0 4px;
    border: 1px solid rgba(0, 0, 0, 0.1);
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.15);

    .user-image {
      width: 100%;
      height: 100%;
      background: #f7f7f7;
      background-size: cover;
    }
  }

  .user-name {
    font-size: 14px;
    margin: 0 4px;
  }
}

.user-panel {
  .dx-list-item .dx-icon {
    vertical-align: middle;
    color: rgba(0, 0, 0, 0.87);
    margin-right: 16px;
  }

  .dx-rtl .dx-list-item .dx-icon {
    margin-right: 0;
    margin-left: 16px;
  }
}

.dx-context-menu.user-menu.dx-menu-base {
  &.dx-rtl {
    .dx-submenu .dx-menu-items-container .dx-icon {
      margin-left: 16px;
    }
  }

  .dx-submenu .dx-menu-items-container .dx-icon {
    margin-right: 16px;
  }

  .dx-menu-item .dx-menu-item-content {
    padding: 3px 15px 4px;
  }
}

.dx-theme-generic .user-menu .dx-menu-item-content .dx-menu-item-text {
  padding-left: 4px;
  padding-right: 4px;
}
</style>
