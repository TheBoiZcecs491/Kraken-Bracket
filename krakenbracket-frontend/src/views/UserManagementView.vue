<template>
    <v-app>
        <div v-if="!loggedIn">
            <NotLoggedIn />
        </div>
        <div v-else-if="loggedIn && this.$store.state.user.accountType === 'System Admin'">
            <h1>User Management</h1>
            <v-container>
                <v-row>
                    <v-col cols="2"></v-col>
                    <v-col cols="4">
                        <h4>Single Create</h4>
                        <v-text-field></v-text-field>
                    </v-col>
                    <v-col cols="4">
                        <h4>Single Delete</h4>
                    </v-col>
                    <v-col cols="2"></v-col>
                </v-row>
            </v-container>
        </div>
    </v-app>
</template>

<script>
import { authComputed } from "../store/helpers.js";
import NotLoggedIn from "../components/NotLoggedIn.vue";

export default {
    components:{
        NotLoggedIn
    },
    computed: {
        ...authComputed,
    },
}
</script>