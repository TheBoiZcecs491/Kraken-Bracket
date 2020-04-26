import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import BracketService from "@/services/BracketService.js";
Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    // user: {
    //   //gamerTagID: 'null',
    //   isLoggedIn: false
    // }
    user: null,
    bracketPlayerInfo: [],
  },
  mutations: {
    // CHANGE_LOGGED_IN_STATUS(state) {
    //   state.user.isLoggedIn = !state.user.isLoggedIn;
    // }
    SET_USER_DATA(state, userData) {
      state.user = userData;
      localStorage.setItem("user", JSON.stringify(userData));
      axios.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${userData.token}`;
    },
    SET_USER_BRACKET_INFO(state, data){
      state.bracketPlayerInfo =data;
      localStorage.setItem("bracketPlayerInfo", JSON.stringify(data));
      axios.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${data.token}`;
    }
  },
  actions: {
    login({ commit }, credentials) {
      return axios.post(
        "https://localhost:44352/api/brackets/login",
        credentials
      ).then(({ data }) => {
        commit("SET_USER_DATA", data);
      });
    },
    bracketPlayerInfo({commit}, email){
      BracketService.getBracketPlayerInfo(email).then(({data}) =>{
        commit("SET_USER_BRACKET_INFO", data);
      })
    }
  },
  getters: {
    loggedIn(state) {
      return !!state.user;
    },
    bracketPlayerInfo(state){
      return state.bracketPlayerInfo
    },
    userInformation(state){
      return state.user
    }
  },
  modules: {}
});
