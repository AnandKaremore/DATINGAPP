import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.services';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
    model: any = {};
    @Output() cancelRegister = new EventEmitter();
    constructor(private authService: AuthService) {}
    ngOnInit() {}

    register() {
        this.authService.register(this.model).subscribe(response => {
            console.log('successfully registered');
        }, error => {
            console.log('failed to register');
        });
    }
    cancel() {
        console.log('cancel');
        this.cancelRegister.emit(false);
    }
}
