import Vue from 'vue';
import App from './App.vue';
import router from "./router";
import store from './store/main';
import axios from 'axios';

import 'devextreme/dist/css/dx.common.css';
import './themes/generated/theme.base.css';
import './themes/generated/theme.additional.css';
import './themes/generated/theme.additional.nav.css';

import appInfo from "./app-info";
import moment from 'moment';

Vue.prototype.$appInfo = appInfo;
Vue.prototype.$moment = moment;
axios.defaults.withCredentials = true
/**
 * Русификация DevExtreme компонентов.
 */
import {locale, loadMessages} from 'devextreme/localization';
import ruMessages from 'devextreme/localization/messages/ru.json';
import config from 'devextreme/core/config';


loadMessages(ruMessages);
locale('ru');

/**
 * Глобальная настройка DevExtreme компонентов.
 */


config({
  forceIsoDateParsing: true
});

/**
 * Инициализация Vue приложения.
 */
window.Vue = Vue;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");