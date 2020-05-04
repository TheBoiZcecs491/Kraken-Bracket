<template>
  
  <div id = "nav">
  <h1>KrakenBracket
  </h1>
    <nav>
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link> |
      <router-link to="/search">Search</router-link> |
      <router-link to="/bracket-list">Bracket List</router-link> |
      <router-link to="/event-list">Event List</router-link> |
      <router-link v-if="!loggedIn" to="/login">Login</router-link>
      <button v-else @click="logout" class="logoutButton">Logout</button>
      <!-- <router-link v-else class="logoutButton" @click="logout" > Logout </router-link> -->
      <h4 v-if="this.$store.state.gamerInfo">Welcome, {{this.$store.state.gamerInfo.gamerTag}}</h4>
    </nav>
  </div>
</template>

<script>
import { authComputed } from "../store/helpers.js";
  export default {
    computed: {
      ...authComputed
    },
    methods: {
      logout() {
        this.$store.dispatch("logout")
      }
    }
  }
</script>

<style>
.logoutButton{
  text-decoration: underline;
  font-weight: bold;
}
</style>