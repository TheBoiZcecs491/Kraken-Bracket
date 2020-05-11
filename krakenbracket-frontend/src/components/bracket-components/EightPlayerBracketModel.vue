<template>
  <div>
    <div v-if="loggedIn">
     
    </div>

    <bracket :rounds="rounds">
      <template #player="{ player }">
        {{ player.name }}
      </template>
    </bracket>
    <!-- <p>{{competitors}}</p> -->
    <h3>Placements:</h3>
    <table class="standings">
      <tr>
        <th>GamerTag</th>
        <th>Score</th>
      </tr>
      <tbody v-for="competitor in competitors" :key="competitor.score">
        <!-- <tr>{{competitor.gamerTag}} {competitor.score}}</tr> -->
        <tr class="">
          <td>{{ competitor.gamerTag }}</td>
          <td>{{ competitor.score }}</td>
        </tr>
      </tbody>
    </table>
    <!-- <ul v-for="competitor in competitors" :key="competitor.score">
            <li><strong>GamerTag</strong> - {{competitor.gamerTag}} | <strong>Score</strong> - {{competitor.score}}</li>
        </ul> -->
         <div
        v-show="
          bracket.host == this.$store.state.gamerInfo.gamerTag &&
            bracket.statusCode == 2
        "
      >
        <v-btn @click="updatePlayerBracketPlacements">Update players</v-btn>
      </div>
  </div>
</template>

<script>
import Bracket from "@/components/bracket-components/Bracket.vue";
import BracketService from "@/services/BracketService.js";
// import axios from "axios";

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
          // Quarter finals
          games: [
            {
              player1: {  name: "?"},
              player2: { name: "?"}
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?"},
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?"}
            }
          ]
        },
        //Semi finals
        {
          games: [
            {
              player1: {  name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            }
          ]
        },
        //Finals
        {
          games: [
            {
              player1: { name: "?" },
              player2: { name: "?" }
            }
          ]
        }
      ]
    };
  },
  created() {
    console.log(this.competitors);
    setTimeout(() => {
      var competitorList = new Array(8);
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
      // Algorithms used to populate the bracket display

      // Quarter finals
      var j = 0;
      for (let i = 0; i < 6; i++) {
        if (i !== 0) i++;
        this.rounds[0].games[j].player1.name = competitorList[i];
        this.rounds[0].games[j].player2.name = competitorList[i + 1];
        j++;
      }

      // Semi finals
      for (let i = 0; i < 4; i++) {
        if (this.competitors[i].score == undefined) continue;
        else {
          if (this.competitors[i].score == 1) {
            if (i % 2 == 0) {
              this.rounds[1].games[0].player1.name = this.competitors[
                i
              ].gamerTag;
            } else {
              this.rounds[1].games[0].player2.name = this.competitors[
                i
              ].gamerTag;
            }
          }
        }
      }
      for (let i = 4; i < 8; i++) {
        console.log(this.competitors[i].score);
        if (this.competitors[i].score == undefined) continue;
        else {
          if (this.competitors[i].score == 1) {
            if (i % 2 == 0) {
              this.rounds[1].games[1].player1.name = this.competitors[
                i
              ].gamerTag;
            } else {
              this.rounds[1].games[1].player2.name = this.competitors[
                i
              ].gamerTag;
            }
          }
        }
      }

      // Finals
      var player1;
      var player2;
      for (let i = 0; i < 8; i++) {
        if (this.competitors[i].score == 2) {
          player1 = this.competitors[i];
          break;
        }
      }
      for (let i = 0; i < 8; i++) {
        if (this.competitors[i].score == 2 && this.competitors[i] !== player1) {
          player2 = this.competitors[i];
          break;
        }
      }
      this.rounds[2].games[0].player1.name = player1.gamerTag;
      this.rounds[2].games[0].player2.name = player2.gamerTag;
    }, 100);

    this.competitors.sort((a, b) =>
      a.score < b.score ? 1 : b.score < a.score ? -1 : 0
    );
  },
  methods: {
    updatePlayerBracketPlacements() {
      var bracketLayer = prompt("Enter the bracket layer number");
      var matchNumber = prompt("Enter the match number");
      var gamerTag = prompt("Enter the gamerTag");
      var playerPlacement = prompt("Player 1 or 2?");
      if (playerPlacement == 1) {
        this.rounds[bracketLayer].games[matchNumber].player1.name = gamerTag;
        for (let i = 0; i < this.competitors.length; i++) {
          console.log("TEST1");
          if (this.competitors[i].gamerTag == gamerTag) {
            BracketService.updateBracketStandings(
              this.competitors[0].bracketID,
              this.competitors[i]
            );
          }
        }
      } else if (playerPlacement == 2) {
        this.rounds[bracketLayer].games[matchNumber].player2.name = gamerTag;
        for (let i = 0; i < this.competitors.length; i++) {
          if (this.competitors[i].gamerTag == gamerTag) {
            console.log("TEST2" + this.competitors[i] == gamerTag);
            BracketService.updateBracketStandings(
              this.competitors[0].bracketID,
              this.competitors[i]
            );
          }
        }
      } else {
        console.log("ERROR");
      }
    }
  }
};
</script>

<style>
.standings {
  width: 50%;
  border: 3px solid black;
}
</style>
