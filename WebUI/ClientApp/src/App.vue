<template>
  <div id="root" class="dx-swatch-arktika-scheme"
       v-if="currentUser.id"
       v-bind:class="{ 'dx-swatch-arktika-scheme-dark': isDark }">
    <div :class="cssClasses">
      <RouterView
          name="layout"
          :navTitle="navTitle"
          :title="title"
          :is-x-small="screen.isXSmall"
          :is-large="screen.isLarge"
      >
        <div class="content">
          <RouterView name="content"/>
        </div>
        <template #footer>
          <Footer/>
        </template>
      </RouterView>
    </div>
  </div>
</template>

<script>
import Footer from "./components/static/Footer";
import {sizes, subscribe, unsubscribe} from "./utils/media-query";
import {mapState} from "vuex";
import axios from 'axios';

function getScreenSizeInfo() {
  const screenSizes = sizes();

  return {
    isXSmall: screenSizes["screen-x-small"],
    isLarge: screenSizes["screen-large"],
    cssClasses: Object.keys(screenSizes).filter(cl => screenSizes[cl])
  };
}

function getFavoriteTheme() {
  return window.localStorage.getItem('favorite-theme') === 'dark'
}

export default {
  name: "App",
  data() {
    return {
      title: this.$appInfo.title,
      navTitle: this.$appInfo.navTitle,
      screen: getScreenSizeInfo(),
      isDark: getFavoriteTheme()
    };
  },
  computed: {
    ...mapState(['currentUser']),
    cssClasses() {
      return ["app"].concat(this.screen.cssClasses);
    }
  },
  methods: {
    screenSizeChanged() {
      this.screen = getScreenSizeInfo();
    }
  },

  mounted() {
    subscribe(this.screenSizeChanged);
  },

  beforeDestroy() {
    unsubscribe(this.screenSizeChanged);
  },
  
  created() {
    axios.interceptors.response.use(
        function(response) {
          return response;
        },
        function(error) {
          if (error.response.status === 401) {
            console.log("ERROR: Invalid token detected")
            window.location.href = "/Identity/Account/Manage";
          }
          throw new Error('Invalid token detected')
        }
    );
    this.$store.dispatch('INIT_CURRENT_USER')
    // this.$store.dispatch('INIT_USER_ROLES_HUB')
  },

  components: {
    Footer
  }
};
</script>

<style lang="scss">
html,
body {
  margin: 0;
  min-height: 100%;
  height: 100%;
}

#root {
  height: 100%;
}

* {
  box-sizing: border-box;
}

.app {
  @import "./themes/generated/variables.base.scss";
  background-color: darken($base-bg, 5);
  display: flex;
  height: 100%;
  width: 100%;
}

.dx-swatch-arktika-scheme-dark {
  .app {
    @import "./themes/generated/variables.additional.scss";
    background-color: lighten($base-bg, 5) !important;
  }
}

</style>
