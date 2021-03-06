import { Component, OnInit, Inject } from '@angular/core';
import { NgForm } from '@angular/forms/src/directives/ng_form';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
 
import { AlertService, /*AuthenticationService*/AuthService } from '../_services/index';

@Component({
  moduleId: module.id,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  {
    title: string;
    form: FormGroup;

    constructor(
    private router: Router,
    private fb: FormBuilder,
    private authservice: AuthService,
    @Inject('BASE_URL') private baseUrl: string){
            
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    /* model: any = {};
    loading = false;
    returnUrl: string;
 
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private alertService: AlertService) { }
 
    ngOnInit() {
        // reset login status
        this.authenticationService.logout();
 
        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/home';
    }
 
    login() {
        this.loading = true;
        this.authenticationService.login(this.model.username, this.model.password)
            .subscribe(
                data => {
                    this.router.navigate([this.returnUrl]);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }*/

/*login(f: NgForm){
  console.log(f.value);
  console.log('Name: ' + f.controls['email'].value);
  console.log('form valid: ' + f.valid);
  console.log('form submitted: ' +f.submitted);
  f.controls['email'].value;
  f.controls['password'].value;
}*/

}
