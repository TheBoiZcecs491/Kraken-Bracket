<template>
    <div>
        <bracket :rounds="rounds">
            <template #player="{ player }">
                {{ player.name }}
            </template>
        </bracket>
        <!-- <p v-for="competitor in competitors" 
        :key="competitor.bracketID" 
        :competitor="competitor">{{competitor.gamerTag}}</p> -->
        <p>{{competitors}}</p>
        <p>{{bracket.bracketID}}</p>
    </div>
  
</template>

<script>
import Bracket from "@/components/bracket-components/Bracket.vue";
import BracketService from "@/services/BracketService.js";

// var competitorList = this.competitors;
// console.log(competitorList);
const rounds = [
  {
    games: [
      {
        player1: { name: "Brian", winner: false },
        player2: { name: "Kevin", winner: true }
      },
       {
        player1: { name: "Fa", winner: false },
        player2: { name: "Simon", winner: true }
      },
    ]
  },
  {
     games: [
      {
        player1: { name: "?", winner: false },
        player2: { name: "?", winner: false }
      }
    ] 
  }
];
export default {
    props: ["id"],
  components: {
    Bracket
  },
  data() {
    return {
      rounds: rounds,
      competitors: []
    };
  },
  created(){
      BracketService.getBracketCompetitorInfo(this.id)
    .then(response => {
        this.competitors = response.data;
        console.log(response);
      })
      .catch(err => {
        // console.log(err);
        this.error = err;
      });
  }
};
</script>
