<template>
    <div>
        <h1>Bracket Listings</h1>
        <!-- the :event sends each prop to the Bracket component -->
        <Bracket v-for="bracket in brackets" :key="bracket.id" :event="bracket"/>
    </div>
</template>

<script>
import Bracket from '@/components/Bracket.model.vue'
import BracketService from '@/services/BracketService.js'
export default {
    components:{
        Bracket
    },
    data(){
        return {
            brackets: []
        }
    },
    created(){
        BracketService.getBrackets()
        .then(response => {
            this.brackets = response.data
            //console.log(response.data)
        })
        .catch(error =>{
            console.log("There was an error: " + error);
        })
    }
}
</script>