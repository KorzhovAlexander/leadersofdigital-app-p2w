<template>
  <div>
    <h2 class="content-block">Домашняя страница</h2>
    <div class="content-block">
      <div class="dx-card responsive-paddings">
        <div class="logos-container">
          <h2 class="content-block">Команда P2W</h2>
        </div>

        <p>
          Мы команда с Архаглельской области, занимаемся web-разработкой. 
        </p>
        <p>Наш состав:</p>
        <ul>
          <li>
            <a href="#" target="_blank" rel="noopener noreferrer">Власов Вячеслав</a> Frontend
          </li>
          <li>
            <a href="#" target="_blank" rel="noopener noreferrer">Выползов Юрий</a> Backend
          </li>
          <li>
            <a href="#" target="_blank" rel="noopener noreferrer">Коржов Александр</a> Fullstack
          </li>
          <li>
            <a href="#" target="_blank" rel="noopener noreferrer">Корнеев Максим</a> Frontend, Design
          </li>
          <li>
            <a href="#" target="_blank" rel="noopener noreferrer">Халабуда Олег</a> Design
          </li>
        </ul>

        <p>Технологии: Vue.js, NetCore 3.1, EFCore, Hangfire</p>
      </div>
    </div>
  </div>
</template>

<style lang="scss">
.logos-container {
  margin: 20px 0 40px 0;
  text-align: center;
  svg {
    display: inline-block;
  }
}

.devextreme-logo {
  width: 200px;
  height: 34px;
  margin-bottom: 17px;
}

.vue-logo {
  width: 180px;
  height: 62px;
}

.plus {
  margin: 20px 10px;
  width: 22px;
  height: 22px;
}

.screen-x-small .logos-container {
  svg {
    width: 100%;
    display: block;
    &.plus {
      margin: 0;
    }
  }
}
</style>
