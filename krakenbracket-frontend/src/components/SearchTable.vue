<template>
        <v-card>
            <v-card-title>
                Search Results
                <v-spacer></v-spacer>
                <v-text-field
                    v-model="filter"
                    label="Filter"
                    clearable
                    single-line
                    hide-details
                ></v-text-field>
            </v-card-title>
            <v-data-table
                    :headers="headers"
                    :items="rows"
                    :search="filter"
                    :items-per-page="10"
            ></v-data-table>
        </v-card>  
</template>

<script>
export default {
    props: {
        headers: [],
        rows: [],
    },
    data() {
        return{
            filter: '',
        }
    }
}
</script>

