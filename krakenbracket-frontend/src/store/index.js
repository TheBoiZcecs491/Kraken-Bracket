import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
//import axios from "axios";
Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    // user: {
    //   //gamerTagID: 'null',
    //   isLoggedIn: false
    // }
    user: null
  },
  mutations: {
    // CHANGE_LOGGED_IN_STATUS(state) {
    //   state.user.isLoggedIn = !state.user.isLoggedIn;
    // }
    SET_USER_DATA(state, userData) {
      state.user = userData;
      localStorage.setItem("user", JSON.stringify(userData))
      Axios.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${userData.token}`
    }
  },
  actions: {
    login({ commit }, credentials) {
      return Axios.post(
        "https://localhost:44352/api/brackets/login",
        credentials
      ).then(({ data }) => {
        commit("SET_USER_DATA", data);
      });
    }
  },
  getters: {
    loggedIn (state){
      return !!state.user
    }
  },
  modules: {}
});
