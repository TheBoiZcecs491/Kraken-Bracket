<template>
  <bracket :rounds="rounds">
    <template #player="{ player }">
      {{ player.name }}
    </template>
  </bracket>
</template>

<script>
import Bracket from "./Bracket";
const rounds = [
  {
    games: [
      {
        player1: { name: "Brian", winner: false },
        player2: { name: "Kevin", winner: false }
      }
    ]
  },
  {
     games: [
      {
        player1: { name: "Brian", winner: false },
        player2: { name: "Kevin", winner: true }
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
