<template>
    <div v-if="bracket">
        <h1>{{bracket.id}}</h1>
    </div>
</template>
<script>
import BracketService from '@/services/BracketService.js'
export default {
    props: ['id'],
    data() {
        return{
            bracket: {}
        }
    },
    created(){
        BracketService.getBrackets(this.id)
        .then(response => {this.bracket = response.data})
    }
}
</script>