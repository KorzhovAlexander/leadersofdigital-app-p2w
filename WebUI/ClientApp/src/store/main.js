import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';
import * as signalR from "@microsoft/signalr";

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        currentUser: {},
        userRolesHub : null,
    },
    getters: {
        currentUser: function (state) {
            return state.currentUser;
        },
        userRolesHub: function (state) {
            return state.userRolesHub;
        },
    },
    mutations: {
        SET_CURRENT_USER: function (state, user) {
            state.currentUser = user;
        },
        SET_USER_ROLES_HUB: function (state, hub) {
            state.userRolesHub = hub;
        },
    },
    actions: {
        INIT_CURRENT_USER: function (context) {
            axios.get(`/api/user/current-user`)
                .then(response => {
                    // eslint-disable-next-line no-console
                    console.log(response.data);
                    context.commit('SET_CURRENT_USER', response.data);
                })
                .catch(function (error) {
                    // eslint-disable-next-line no-console
                    console.log(error);
                });
        },
        INIT_USER_ROLES_HUB: function (context) {
            let hubConnection = new signalR.HubConnectionBuilder()
                .withUrl(`/hub/user-roles`)
                .configureLogging(signalR.LogLevel.Warning)
                .build();
            //Запуск и настройка
            hubConnection
                .start()
                .then(() => {
                    hubConnection.on('UserRoleUpdated', (data) => {
                        console.log(data);
                        // изменяю роль
                    });
                    hubConnection.on('UserRoleInserted', (data) => {
                        console.log(data);
                        // добавляю роль
                    });
                    hubConnection.on('UserRoleDelete', (key) => {
                        console.log(key);
                        // удаляю роль
                    })
                })
            context.commit('SET_USER_ROLES_HUB', hubConnection);
        },
    },
    modules: {}
});

export default store;
