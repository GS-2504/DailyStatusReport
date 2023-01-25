import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators,FormArray } from '@angular/forms';
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
  
})
export class UserComponent implements OnInit {
  form:FormGroup
  nestedForms: FormArray;
  constructor() {
    this.form = new FormGroup({
      taskname: new FormControl('',[Validators.required]),
      date: new FormControl(),
      taskhour: new FormControl(),
      success: new FormControl(),
      obstacle: new FormControl(),
      nextdayplan: new FormControl(),
      nestedForms: new FormArray([])
    });
   }

  ngOnInit(): void {
  }

}
