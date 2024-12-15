import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { UserRegistrationComponent } from './user-registration/user-registration.component';

export const routes: Routes = [
    // Add routes here later if needed
    { path: '', component: UserRegistrationComponent },
    { path: 'user/:id', component: ConfirmationComponent },
    { path: '**', redirectTo: '' }
];
