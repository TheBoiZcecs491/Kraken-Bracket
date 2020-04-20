<template>
    <v-app>
        <div>
            <v-btn @click="logInUser" color="success">Log In</v-btn>
        </div>
    </v-app>
</template>

<script>
export default {
    methods: {
        logInUser(){
            this.$store.commit('CHANGE_LOGGED_IN_STATUS')
        }
    }
}
</script>