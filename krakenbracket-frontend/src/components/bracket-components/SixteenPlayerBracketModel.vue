<template>
  <div>
    <bracket :rounds="rounds">
      <template #player="{ player }">
        {{ player.name }}
      </template>
    </bracket>
    <h3>Placements:</h3>
    <table class="standings">
      <tr>
        <th>GamerTag</th>
        <th>Score</th>
      </tr>
      <tbody v-for="competitor in competitors" :key="competitor.score">
        <!-- <tr>{{competitor.gamerTag}} {competitor.score}}</tr> -->
        <tr>
          <td>{{ competitor.gamerTag }}</td>
          <td>{{ competitor.score }}</td>
        </tr>
      </tbody>
    </table>
    <br />
    <div
        v-show="
          bracket.host === this.$store.state.gamerInfo.gamerTag &&
            bracket.statusCode === 2
        "
      >
        <v-btn @click="updatePlayerBracketPlacements">Update players</v-btn>
    </div>
    <br />
  </div>
</template>

<script>
import Bracket from "@/components/bracket-components/Bracket.vue";
import BracketService from "@/services/BracketService.js";
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
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            }
          ]
        },
        {
          games: [
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            }
          ]
        },
        {
          games: [
            {
              player1: { name: "?" },
              player2: { name: "?" }
            },
            {
              player1: { name: "?" },
              player2: { name: "?" }
            }
          ]
        },
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
    setTimeout(() => {
      var competitorList = new Array(16);
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
      for (let i = 0; i < 4; i++) {
        if (i !== 0) i++;
        this.rounds[0].games[j].player1.name = competitorList[i];
        this.rounds[0].games[j].player2.name = competitorList[i + 1];
        j++;
      }
      // Finals
      for (let i = 0; i < 2; i++) {
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
    }, 50);
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
