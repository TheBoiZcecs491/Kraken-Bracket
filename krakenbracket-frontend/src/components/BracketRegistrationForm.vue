<template>
    <v-app>
        <div>
            <h1>Signup</h1>
            <v-form>
                <v-container fluid>
                    <v-layout row>
                        <v-flex xs4>
                            <!-- This element's content is intentionally empty -->
                        </v-flex>
                        <v-flex xs4>
                            <v-text-field label="Email" type="email" placeholder="john@gmail.com"></v-text-field>
                            <v-text-field label="GamerTag" type="text"></v-text-field>
                            <v-btn color="primary">Register!</v-btn>
                        </v-flex>
                        <v-flex xs4>
                            <!-- This element's content is intentionally empty -->
                        </v-flex>
                    </v-layout>
                </v-container>  
            </v-form>
        </div>
    </v-app>

</template>

<script>
    export default {
        data() {
            return {
                dialog: false
            };
        }
    };
</script>