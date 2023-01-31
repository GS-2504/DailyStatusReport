import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators,FormArray, FormBuilder } from '@angular/forms';
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
  
})
export class UserComponent implements OnInit {
  form:FormGroup
  
  constructor(private formbuilder:FormBuilder) {
   }

  ngOnInit(): void {
      this.form = this.formbuilder.group({
        tasks:this.formbuilder.array([this.userTaskForm()])
      })
    }
  userTaskForm():FormGroup{
      return this.formbuilder.group({
      taskname:['',[Validators.required]],
      date:['',[Validators.required]],
      hours:['',Validators.required],
      success:['',Validators.required],
      obstacle:['',Validators.required],
      nextdayplan:['',Validators.required]
    })
  }

  addMoreTask(){
    const control= this.form.controls['tasks'] as FormArray
    control.push(this.userTaskForm());
  }
  removeTask(i:number){
    const control= this.form.controls['tasks'] as FormArray
    control.removeAt(i);
  }
  onSave(){
      console.log(this.form.value);
  }

  get tasks(){
    return this.form.get('tasks') as FormArray;
  }
}