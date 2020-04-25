<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <HomePage msg="Welcome to Kraken Bracket" />
    <div v-if="!loggedIn">
      <p>
        <router-link to="/login">Log in</router-link> to access bracket and
        event registration
      </p>
    </div>
  </div>
</template>


<script>
// @ is an alias to /src
import HomePage from "@/components/HomePage.vue";
import { authComputed } from "../store/helpers.js";

export default {
  name: "Home",
  components: {
    HomePage
  },
  computed: {
    ...authComputed
  }
};
</script>
