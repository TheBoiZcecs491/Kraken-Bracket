<template>
  <v-app>
    <div>
      <h1 id="title">{{ bracket.bracketName }}</h1>
      <h3>Start Date: {{ bracket.startDate }}</h3>
      <h3>End Date: {{ bracket.endDate }}</h3>
      <h4>Number of players: {{ bracket.playerCount }}</h4>
      <h4>Game: {{ bracket.gamePlayed }}</h4>
      <h4>Gaming platform: {{ bracket.gamingPlatform }}</h4>
      <div>
        <h4>Rules:</h4>
        <p>{{ bracket.rules }}</p>
      </div>
      <router-link :to="{ name: 'bracket-registration', params: { id: bracket.bracketID } }">
        <v-btn type="submit">Register!</v-btn>
      </router-link>
    </div>
  </v-app>
</template>

<script>
import BracketService from "@/services/BracketService.js";

export default {
  props: ["id"],
  data() {
    return {
      bracket: {}
    };
  },
  created() {
    BracketService.getBracketByID(this.id)
      .then(response => {
        this.bracket = response.data;
      })
      .catch(error => {
        console.log("Error: " + error.response);
      });
  }
};
</script>

<style scoped>
#title {
  font-size: 75px;
  font-weight: 800;
}
</style>
