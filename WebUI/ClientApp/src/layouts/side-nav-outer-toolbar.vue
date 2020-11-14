<template>
  <div class="side-nav-outer-toolbar">
    <header-toolbar
        class="layout-header"
        :menu-toggle-enabled="true"
        :toggle-menu-func="toggleMenu"
        :title="navTitle"
    />
    <dx-drawer
        class="layout-body"
        position="before"
        template="menu"
        :opened.sync="menuOpened"
        :opened-state-mode="drawerOptions.menuMode"
        :reveal-mode="drawerOptions.menuRevealMode"
        :min-size="drawerOptions.minMenuSize"
        :shading="drawerOptions.shaderEnabled"
        :close-on-outside-click="drawerOptions.closeOnOutsideClick"
    >
      <dx-scroll-view ref="scrollViewRef" class="with-footer">
        <slot/>
        <slot name="footer"/>
      </dx-scroll-view>
      <!-- eslint-disable vue/no-unused-vars -->
      <side-nav-menu
          slot="menu"
          slot-scope="_"
          :compact-mode="!menuOpened"
          @click="handleSideBarClick"
      />
      <!-- eslint-enable -->
    </dx-drawer>
  </div>
</template>

<script>
import DxDrawer from "devextreme-vue/drawer";
import DxScrollView from "devextreme-vue/scroll-view";
import DxToolbar, {DxItem} from "devextreme-vue/toolbar";

import menuItems from "../app-navigation";
import HeaderToolbar from "../components/static/NavBar";
import SideNavMenu from "../components/static/SideNavMenu";

export default {
  props: {
    navTitle: String,
    title: String,
    isXSmall: Boolean,
    isLarge: Boolean
  },
  methods: {
    toggleMenu(e) {
      console.log("CLICK")
      console.log("CLICK")

      const pointerEvent = e.event;
      pointerEvent.stopPropagation();
      if (this.menuOpened) {
        this.menuTemporaryOpened = false;
      }
      this.menuOpened = !this.menuOpened;
    },
    handleSideBarClick() {
      console.log("CLICK")
      if (this.menuOpened === false) this.menuTemporaryOpened = true;
      this.menuOpened = true;
    }
  },
  data() {
    return {
      menuOpened: false,
      menuTemporaryOpened: false,
      menuItems
    };
  },
  computed: {
    drawerOptions() {
      const shaderEnabled = !this.isLarge;
      return {
        menuMode: this.isLarge ? "shrink" : "overlap",
        menuRevealMode: this.isXSmall ? "slide" : "expand",
        minMenuSize: this.isXSmall ? 0 : 60,
        menuOpened: this.isLarge,
        closeOnOutsideClick: shaderEnabled,
        shaderEnabled
      };
    },
    headerMenuTogglerEnabled() {
      return this.isXSmall;
    },
    scrollView() {
      return this.$refs["scrollViewRef"].instance;
    }
  },
  watch: {
    isLarge() {
      if (!this.menuTemporaryOpened) {
        this.menuOpened = this.isLarge;
      }
    },
    $route() {
      if (this.menuTemporaryOpened || !this.isLarge) {
        this.menuOpened = false;
        this.menuTemporaryOpened = false;
      }
      this.scrollView.scrollTo(0);
    }
  },
  components: {
    DxDrawer,
    DxScrollView,
    HeaderToolbar,
    SideNavMenu
  }
};
</script>

<style lang="scss">
.side-nav-outer-toolbar {
  flex-direction: column;
  display: flex;
  height: 100%;
  width: 100%;
}

.container {
  height: 100%;
  flex-direction: column;
  display: flex;
}

.layout-header {
  z-index: 1501;
}

.layout-body {
  flex: 1;
  min-height: 0;
}

.content {
  flex-grow: 1;
}

#navigation-header {
  @import "../themes/generated/variables.additional.scss";
  background-color: $base-accent;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24);

  .menu-button .dx-icon {
    color: $base-text-color;
  }

  .screen-x-small & {
    padding-left: 20px;
  }

  .dx-theme-generic & {
    padding-top: 10px;
    padding-bottom: 10px;
  }
}
</style>
