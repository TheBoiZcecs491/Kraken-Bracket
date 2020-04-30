<template>
    <div class="home">
      <SearchBar></SearchBar>
      <img alt="Vue logo" src="../assets/logo.png" />
      <HelloWorld msg="Welcome to Kraken Bracket" />
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
import HelloWorld from "@/components/HelloWorld.vue";
import SearchBar from "@/components/SearchBar.vue";
import { authComputed } from "../store/helpers.js";
export default {
  name: "Home",
  components: {
    HelloWorld,
    SearchBar
  },
  computed: {
    ...authComputed
  }
};
</script>
