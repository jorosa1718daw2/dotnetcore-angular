import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }    from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
// used to create fake backend
import { fakeBackendProvider } from './_helpers/fake-backend';

import { AppComponent }  from './app.component';
import { routing }        from './app.routing';


import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AlertComponent } from './_directives/alert.component';
import { AuthGuard } from './_guards/index';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { AlertService, /*AuthenticationService*/AuthService, UserService } from './_services/index';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FocusComponent } from './focus/focus.component';
import { SensorRealTimeDataComponent } from './sensor-rt-data/sensor-rt-data.component';
import { DataService } from './_services/data.service';
import { CurrentSensorDataService } from './services/current-sensor-data.service';
import { FocusService } from './services/focus.service';


@NgModule({
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        FormsModule,
        HttpClientModule,
        routing
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        FocusComponent,
        SensorRealTimeDataComponent
    ],
    providers: [
        AuthGuard,
        AlertService,
        //AuthenticationService,
        AuthService,
        UserService,
        CurrentSensorDataService,
        FocusService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: JwtInterceptor,
            multi: true
        },
        DataService,
        // provider used to create fake backend
       // fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }