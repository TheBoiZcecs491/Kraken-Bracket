<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link> |
      <router-link to="/search">Search</router-link> |
      <router-link to="/bracket-list">Bracket List</router-link> |
      <router-link to="/event-list">Event List</router-link>
      <!-- <router-link to="/login">Login</router-link> -->
      <h4 v-if="this.$store.state.gamerInfo">Welcome, {{this.$store.state.gamerInfo.gamerTag}}</h4>
    </div>
    <router-view />
  </div>
</template>

<script>
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #80308d;
}
</style>
