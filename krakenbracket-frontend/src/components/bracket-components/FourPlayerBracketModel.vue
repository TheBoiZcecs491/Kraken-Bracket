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
        <!-- <p>{{bracket}}</p> -->
    </div>
  
</template>

<script>
import Bracket from "@/components/bracket-components/Bracket.vue";
export default {
  props:{
    competitors: Array
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
]
    };
  },

};
</script>
