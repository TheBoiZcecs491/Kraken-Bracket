<template>
    <v-app>
        <div v-if="!loggedIn">
            <NotLoggedIn />
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
        ...authComputed
    },
}
</script>