import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
    model: any = {};
    @Input() valuesfromHome;
    @Output() cancelRegister = new EventEmitter();
constructor() {}
ngOnInit() {}

register() { }
cancel() {
    console.log('cancel');
    this.cancelRegister.emit(false);
 }
}
