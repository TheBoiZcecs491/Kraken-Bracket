<template>
  <bracket :rounds="rounds">
    <template #player="{ player }">
      {{ player.name }}
    </template>
  </bracket>
</template>

<script>
import Bracket from "@/components/bracket-components/Bracket.vue";
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
        player1: { name: "Kevin", winner: false },
        player2: { name: "Simon", winner: true }
      }
    ] 
  }
];
export default {
  components: {
    Bracket
  },
  data() {
    return {
      rounds: rounds
    };
  }
};
</script>
