<template>
  <div>
    <div v-show="bracket.host === this.$store.state.gamerInfo.gamerTag">
            <v-btn @click="updatePlayerBracketPlacements">Update players</v-btn>
        </div>
    <bracket :rounds="rounds">
      <template #player="{ player }">
        {{ player.name }}
      </template>
    </bracket>
    <h3>Placements:</h3>
    <ul v-for="competitor in competitors" :key="competitor.score">
        <li>{{competitor.gamerTag}}  {{competitor.score}}</li>
    </ul>
  </div>
</template>

<script>
import Bracket from "@/components/bracket-components/Bracket.vue";
export default {
  props: {
    competitors: Array,
    bracket: Object
  },
  components: {
    Bracket
  },
  data() {
    return {
      rounds: [
        {
          games: [
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
            
          ]
        },
        {
          games: [
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
          ]
        },
        {
           games: [
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
          ]
        },
        {
           games: [
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
          ]
        },
        {
             games: [
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                },
                 {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
             ]
        },
        {
             games: [
                {
                    player1: {  name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
             ]
        }
      ]
    };
  },
  created() {
    setTimeout(() => {
      var competitorList = new Array(32);
      //   console.log(competitorList);
      for (let index = 0; index < this.competitors.length; index++) {
        competitorList[index] = this.competitors[index].gamerTag;
      }
      for (
        let index = this.competitors.length;
        index < competitorList.length;
        index++
      ) {
        competitorList[index] = "?";
      }
      // Algorithm used to populate the bracket display
      var j = 0;
      for (let i = 0; i < 30; i++) {
        if (i !== 0) i++;
        this.rounds[0].games[j].player1.name = competitorList[i];
        this.rounds[0].games[j].player2.name = competitorList[i + 1];
        j++;
      }

      // Quarter finals
      for (let i = 0; i < 16; i++) {
              if(this.competitors[i].score == undefined) continue;
              else{
                  if(this.competitors[i].score == 1){
                  if(i % 2 == 0){
                      this.rounds[1].games[0].player1.name = this.competitors[i].gamerTag;
                  }
                  else {
                      this.rounds[1].games[0].player2.name = this.competitors[i].gamerTag;
                  }
              }
          }
        }
      
    }, 50);
  },
  methods: {
    updatePlayer1BracketPlacements() {
      var bracketLayer = prompt("Enter the bracket layer number");
      var matchNumber = prompt("Enter the match number");
      var gamerTag = prompt("Enter the gamerTag");
      console.log(gamerTag + " <-----");
      console.log(this.rounds[bracketLayer].games[matchNumber].player1.name);
      this.rounds[bracketLayer].games[matchNumber].player1.name = gamerTag;
    }
  }
};
</script>
