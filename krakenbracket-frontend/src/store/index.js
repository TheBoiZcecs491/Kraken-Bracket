import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import BracketService from "@/services/BracketService.js";
Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    user: null,
    bracketPlayerInfo: [],
    gamerInfo: null
  },
  mutations: {
    SET_USER_DATA(state, userData) {
      state.user = userData;
      localStorage.setItem("user", JSON.stringify(userData));
      axios.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${userData.token}`;
    },
    CLEAR_USER_DATA(){
      localStorage.removeItem("user")
      location.reload();
    },
    SET_USER_BRACKET_INFO(state, data) {
      state.bracketPlayerInfo = data;
      localStorage.setItem("bracketPlayerInfo", JSON.stringify(data));
      axios.defaults.headers.common["Authorization"] = `Bearer ${data.token}`;
    },
    SET_USER_GAMER_INFO(state, data){
      state.gamerInfo = data;
      localStorage.setItem("gamerInfo", JSON.stringify(data));
      axios.defaults.headers.common["Authorization"] = `Bearer ${data.token}`;
    }
  },
  actions: {
    login({ commit }, credentials) {
      return axios
        .post("https://localhost:44352/api/login", credentials)
        .then(({ data }) => {
          commit("SET_USER_DATA", data);
        });
    },
    logout({commit}){
      commit('CLEAR_USER_DATA')
    },
    bracketPlayerInfo({ commit }, email) {
      BracketService.getBracketPlayerInfo(email).then(({ data }) => {
        commit("SET_USER_BRACKET_INFO", data);
      });
    },
    gamerInfo({ commit }, email){
      BracketService.getGamerInfo(email).then(({data}) => {
        commit("SET_USER_GAMER_INFO", data)
      })
    },
  },
  getters: {
    loggedIn(state) {
      return !!state.user;
    },
    bracketPlayerInfo(state) {
      return state.bracketPlayerInfo;
    },
    userInformation(state) {
      return state.user;
    },
    gamerInfo(state){
      return state.gamerInfo
    }
  },
  modules: {}
});
