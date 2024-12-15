import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

// Bootstrap the application
// The `appConfig` is a configuration object that contains the base URL of the API
bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
