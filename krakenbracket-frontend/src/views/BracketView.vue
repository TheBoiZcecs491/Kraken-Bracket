<template>
<<<<<<< HEAD
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
=======
    <div v-if="bracket">
        <h1>{{bracket.id}}</h1>
>>>>>>> parent of 1f5a6a7... Bracket Info Displayed (via URL)
    </div>
    <button>Register!</button>
  </div>
</template>
<script>
import BracketService from "@/services/BracketService.js";
export default {
<<<<<<< HEAD
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
  font-size: 100px;
  font-weight: 800;
}
</style>
=======
    props: ['id'],
    data() {
        return{
            bracket: {}
        }
    },
    created(){
        BracketService.getBrackets(this.id)
        .then(response => {this.bracket = response.data})
    }
}
</script>
>>>>>>> parent of 1f5a6a7... Bracket Info Displayed (via URL)
