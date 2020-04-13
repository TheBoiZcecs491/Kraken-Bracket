<template>
    <div>
        <h1>Bracket Listings</h1>
        <Bracket v-for="bracket in brackets" :key="bracket.id" :event="bracket"/>
    </div>
</template>

<script>
import Bracket from '@/components/Bracket.model.vue'
import axios from 'axios'
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
        axios
        .get('http://localhost:3000/brackets')
        .then(response => {
            this.brackets = response.data
        })
        .catch(error =>{
            console.log("There was an error: " + error);
        })
    }
}
</script>