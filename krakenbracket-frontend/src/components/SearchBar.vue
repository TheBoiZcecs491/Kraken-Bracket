<template>
    <div>
        <v-text-field
            class="search-input"
            v-model="search"
            type="search"
            placeholder="Search..."
            required
            clearable
        >
        </v-text-field>
    </div>
</template>

<script>
export default {
    name: "SearchBar",
    data() {
        return {
            search: '',
        }
    },
}
</script>