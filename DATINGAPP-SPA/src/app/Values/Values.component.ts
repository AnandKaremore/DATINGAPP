import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Component({
    selector: 'app-values',
    templateUrl: './Values.component.html'
})

export class ValuesComponent implements OnInit {
    Values:any;
    constructor(private http:HttpClient) { }
    getValues(){
        this.http.get("http://localhost:5000/api/Values").subscribe(response=>{
            this.Values = response;
        });
    }
    ngOnInit() { 
        this.getValues();
    }
    
    
}