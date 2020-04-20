import Vue from "vue";
import Vuex from "vuex";
//import axios from "axios";
Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    user: {
      //gamerTagID: 'null',
      isLoggedIn: false
    }
  },
  mutations: {
    CHANGE_LOGGED_IN_STATUS(state){
      state.user.isLoggedIn = !state.user.isLoggedIn
    }
  },
  actions: {},
  getters:{},
  modules: {}
});
