import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import vuetify from "./plugins/vuetify";
import store from "./store";
import Vuelidate from "vuelidate";
import DatetimePicker from "vuetify-datetime-picker";

Vue.use(Vuelidate);
Vue.use(DatetimePicker);

Vue.config.productionTip = false;

new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount("#app");
