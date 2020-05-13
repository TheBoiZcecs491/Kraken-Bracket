<template>
  <v-app>
    <div>
      <h1>Bracket Listings</h1>
      <div v-if="loggedIn" >
      <router-link to="/new-bracket" class="create-btn">
        <v-btn class="primary" x-large >Create a new Event</v-btn>
      </router-link>
    </div>
    <div v-else>
      <router-link to="/login" class="create-btn">
        <v-btn class="primary" x-large >Create a new Event</v-btn>
      </router-link>
    </div>
      <!-- the :event sends each prop to the Bracket component -->
      <BracketModel
        v-for="bracket in brackets"
        :key="bracket.id"
        :bracket="bracket"
      />
    </div>
  </v-app>
</template>

<script>
import BracketModel from "@/components/BracketModel.vue";
import BracketService from "@/services/BracketService.js";
import { authComputed } from "../store/helpers.js";
export default {
  components: {
    BracketModel
  },
  data() {
    return {
      brackets: []
    };
  },
  computed: {
    ...authComputed
  },
  created() {
    BracketService.getBrackets()
      .then(response => {
        this.brackets = response.data;
        //console.log(response.data)
      })
      .catch(error => {
        console.log("There was an error: " + error);
      });
  }
};
</script>

<style>
.create-btn {
  text-decoration: none;
}
</style>
