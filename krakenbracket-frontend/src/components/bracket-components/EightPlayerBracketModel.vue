<template>
    <div>
        <bracket :rounds="rounds">
            <template #player="{ player }">
                {{ player.name }}
            </template>
        </bracket>
        <p>{{competitors}}</p>
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
              // Quarter finals
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
                }
            ]
        },
        //Semi finals
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
        //Finals
        {
            games: [
                {
                    player1: { name: "?", winner: false },
                    player2: {  name: "?", winner: false }
                }
            ]
        }]
    }
  },
  created(){
      var competitorList = new Array(8);
    //   console.log(competitorList);
    for (let index = 0; index < this.competitors.length; index++) {
        competitorList[index] = this.competitors[index].gamerTag;
    }
     for (let index = this.competitors.length; 
            index < competitorList.length; index++) {
        competitorList[index] = "?";
    }
    // this.rounds[0].games[0].player1.name = competitorList[0];
    // this.rounds[0].games[0].player2.name = competitorList[1];
    // this.rounds[0].games[1].player1.name = competitorList[2];
    // this.rounds[0].games[1].player2.name = competitorList[3];
    // this.rounds[0].games[2].player1.name = competitorList[4];
    // this.rounds[0].games[2].player2.name = competitorList[5];
    // this.rounds[0].games[3].player1.name = competitorList[6];
    // this.rounds[0].games[3].player2.name = competitorList[7];
  
    var j = 0;
    for (let i = 0; i < 6; i++) {
        if(i !== 0) i++;
        this.rounds[0].games[j].player1.name = competitorList[i];
        this.rounds[0].games[j].player2.name = competitorList[i + 1];
        j++;
    }
  }
}
</script>
