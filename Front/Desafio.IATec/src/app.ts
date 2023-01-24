import { PLATFORM } from 'aurelia-pal';
import { RouterConfiguration, Router } from 'aurelia-router';

export class App {

  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    this.router = router;
    config.title = 'Aurelia';
    config.map([
      { route: 'registro', name: 'registro', moduleId: PLATFORM.moduleName('components/Registro/registro'), nav: true, title: 'Registro' },
      { route: ['', 'buscar'], name: 'buscar', moduleId: PLATFORM.moduleName('components/Buscar/buscar'), nav: true, title: 'Buscar' },
    ]);
  }
}
