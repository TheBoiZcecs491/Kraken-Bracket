<template>
        <v-card>
            <v-card-title>
                {{ keyword }}
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
            ></v-data-table>
        </v-card>  
</template>

<script>
export default {
    props: {
        headers: [],
        rows: [],
        keyword: String
    },
    data() {
        return{
            filter: '',
        }
    }
}
</script>

