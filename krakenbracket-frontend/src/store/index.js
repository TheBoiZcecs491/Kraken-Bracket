import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import BracketService from "@/services/BracketService.js";
import EventService from "../services/EventService";
Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    user: null,
    bracketPlayerInfo: [],
    gamerInfo: null,
    eventPlayerInfo: []
  },
  mutations: {
    SET_USER_DATA(state, userData) {
      state.user = userData;
      localStorage.setItem("user", JSON.stringify(userData));
      axios.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${userData.token}`;
      //protip: save this value to a file and read from it.
      // that way you can refresh or open/close the browser and stay loggedin.
      // I noticed refreshing or surfing to other pages on the front end resets this.
    },
    CLEAR_USER_DATA() {
      localStorage.removeItem("user");
      location.reload();
    },
    SET_USER_BRACKET_INFO(state, data) {
      state.bracketPlayerInfo = data;
      localStorage.setItem("bracketPlayerInfo", JSON.stringify(data));
      axios.defaults.headers.common["Authorization"] = `Bearer ${data.token}`;
    },
    SET_USER_GAMER_INFO(state, data) {
      state.gamerInfo = data;
      localStorage.setItem("gamerInfo", JSON.stringify(data));
      axios.defaults.headers.common["Authorization"] = `Bearer ${data.token}`;
    },
    SET_EVENT_PLAYER_INFO(state, data) {
      state.eventPlayerInfo = data;
      localStorage.setItem("eventPlayerInfo", JSON.stringify(data));
      axios.defaults.headers.common["Authorization"] = `Bearer ${data.token}`;
    },
    CLEAR_EVENT_PLAYER_INFO() {
      localStorage.removeItem("eventPlayerInfo");
    }
  },
  actions: {
    login({ commit }, credentials) {
      console.log(credentials);
      return axios
        .post("https://localhost:44352/api/login", credentials)
        .then(({ data }) => {
          commit("SET_USER_DATA", data);
        });
    },
    registerUser({ commit }, formFill) {
      return axios
        .post("https://localhost:44352/api/register", formFill)
        .then(({ data }) => {
          commit("SET_USER_DATA", data);
        });
    },
    updateUser({ commit }, formFill) {
      return axios
        .post("https://localhost:44352/usermanagement/updateprofile", formFill)
        .then(({ data }) => {
          commit("SET_USER_DATA", data);
        });
    },
    logout({ commit }) {
      commit("CLEAR_USER_DATA");
    },
    bracketPlayerInfo({ commit }, email) {
      BracketService.getBracketPlayerInfo(email).then(({ data }) => {
        commit("SET_USER_BRACKET_INFO", data);
      });
    },
    gamerInfo({ commit }, email) {
      BracketService.getGamerInfo(email).then(({ data }) => {
        commit("SET_USER_GAMER_INFO", data);
      });
    },
    eventPlayerInfo({ commit }, eventID) {
      EventService.getEventInfo(eventID).then(({ data }) => {
        commit("SET_EVENT_PLAYER_INFO", data);
      });
    },
    removeEventPlayerInfo({ commit }) {
      commit("CLEAR_EVENT_PLAYER_INFO");
    }
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
    gamerInfo(state) {
      return state.gamerInfo;
    },
    eventPlayerInfo(state) {
      return state.eventPlayerInfo;
    }
  },
  modules: {}
});
