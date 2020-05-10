<template>
    <div>
        <button @click="updatePlayerBracketPlacements">Update players</button>
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
import BracketService from "@/services/BracketService.js";
// import axios from "axios";

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
                    player1: { id: "1", name: "?", winner: true },
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
                    player1: { id: "1", name: "?", winner: false },
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
      setTimeout(() => {
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
    // Algorithm used to populate the bracket display
    var j = 0;
    for (let i = 0; i < 6; i++) {
        if(i !== 0) i++;
        this.rounds[0].games[j].player1.name = competitorList[i];
        this.rounds[0].games[j].player2.name = competitorList[i + 1];
        j++;
    }

      }, 100);
  },
  methods:{
      updatePlayerBracketPlacements(){
          var bracketLayer = prompt("Enter the bracket layer number");
          var matchNumber = prompt("Enter the match number");
          var gamerTag = prompt("Enter the gamerTag");
          var playerPlacement = prompt("Player 1 or 2?");
          console.log(gamerTag + " <-----");
          console.log(this.rounds[bracketLayer].games[matchNumber].player1.name);
          if(playerPlacement == 1){
              this.rounds[bracketLayer].games[matchNumber].player1.name = gamerTag;
              BracketService.updateBracketStandings(this.competitors[0].bracketID, this.competitors[0])
            //   var bracketID = this.competitors[0].bracketID;
            //   var bracketIDParse = parseInt(bracketID);
            // axios.post(`https://localhost:44352/api/brackets/${this.bracket.bracketID}/register/${this.competitors[0]}`)
              
          }
          else if (playerPlacement == 2){
              this.rounds[bracketLayer].games[matchNumber].player2.name = gamerTag;
              for (let i = 0; i < this.competitors.length; i++) {
                  if (this.competitors[i] == gamerTag){
                      BracketService.updateBracketStandings(this.competitors[0].bracketID, this.competitors[i])
                  }
              }
          }
          else {
              console.log("BAD. THAT'S A NO NO");
          }
          
        //   if(matchNumber % 2 == 0 ){
        //       if(bracketLayer == (this.rounds.length - 1)){
        //         if(this.rounds[bracketLayer].games[matchNumber].player1.name == gamerTag){
        //         this.rounds[bracketLayer].games[matchNumber];
        //     }
        //   } 
        //   else{
        //       if(this.rounds[bracketLayer].games[matchNumber].player1.name == gamerTag){
        //       this.rounds[bracketLayer].games[matchNumber+1];
        //     }
        //   }
       
        
        // if(bracketLayer === (this.rounds.length - 1)){ // for grand finals
        	
        // }
        
        // else if(matchNumber % 2 === 0 ){ // if odd match, progress into next round's player1
        //     if(this.rounds[bracketLayer].games[matchNumber].player1.name === gamerTag){
        //       this.rounds[bracketLayer].games[matchNumber + 1].player1.name = gamerTag;
        //     }
        //     else{
        //       this.rounds[bracketLayer].games[matchNumber + 1].player2.name = gamerTag;
        //     }
        //   } 
        // else{ // if even match, progress into next round as player2
        //     if(this.rounds[bracketLayer].games[matchNumber].player1.name === gamerTag){
        //       this.rounds[bracketLayer].games[matchNumber + 1].player1.name = gamerTag;
        //     }
        //     else{
        //       this.rounds[bracketLayer].games[matchNumber + 1].player2.name = gamerTag;
        //     }
        // }
      }
  }
}
</script>
