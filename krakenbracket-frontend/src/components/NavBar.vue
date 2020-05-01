<template>
  <div id = "nav">
  <h1>KrakenBracket
  </h1>
    <nav>
      <router-link to="/">Home</router-link> |
      <router-link to="/about">About</router-link> |
      <router-link to="/search-results/:search">Search</router-link> |
      <router-link to="/bracket-list">Bracket List</router-link> |
      <router-link to="/event-list">Event List</router-link>
      <!-- <router-link to="/login">Login</router-link> -->
    </nav>
  </div>
</template>

<script>
  export default {
    
  }
</script>

<style scoped>

</style>