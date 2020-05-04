<template>
    <div>
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
                    rounded
                    solo
                ></v-text-field>
            </v-card-title>
            <v-data-table
                    :headers="headers"
                    :items="search_result"
                    :search="filter"
                    :items-per-page="10"
            ></v-data-table>
        </v-card>
    </div>
</template>

<script>
export default {
    props: {
        headers: [],
        search_result: [],
    },
    data() {
        return{
            details: '',
            filter: '',
        }
    },
}
</script>

