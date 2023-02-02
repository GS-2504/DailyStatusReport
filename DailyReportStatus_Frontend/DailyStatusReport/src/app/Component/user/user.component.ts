import { Component, OnInit,OnDestroy } from '@angular/core';
import { FormGroup,Validators, FormArray, FormBuilder } from '@angular/forms';
import { Usertaskdetailsdto } from 'src/app/model/usertaskdetailsdto';
import { Usertaskdto } from 'src/app/model/usertaskdto';
import { AuthService } from 'src/app/services/auth.service';
import { Subject } from 'rxjs';
import { Status } from 'src/app/model/usertaskdetailsdto';
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],

})
export class UserComponent implements OnInit,OnDestroy {
  form: FormGroup
  usertask: Usertaskdto = new Usertaskdto();
  usertasks: Usertaskdto[] = [];
  userTaskDetails:Usertaskdetailsdto[]=[];
  datatOptions:DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  status:Status
  constructor(private formbuilder: FormBuilder, private service: AuthService) {
  }

  ngOnInit(): void {
    this.datatOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      processing: true
    };
    this.service.getAllUserTaskDetails(this.service.userId()).subscribe(
      (response) => {
       this.userTaskDetails = response;
       this.dtTrigger.next(null);
       this.service.getAllUserTaskDetails(this.service.userId());
      },
      (error) => {
        console.log(error);
      }
    )
    this.form = this.formbuilder.group({
      tasks: this.formbuilder.array([this.userTaskForm()])
    })
   
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
  
  userTaskForm(): FormGroup {
    return this.formbuilder.group({
      userId:[this.service.userId()],
      taskName: ['', [Validators.required]],
      taskDate: ['', [Validators.required]],
      taskHours: ['', Validators.required],
      success: ['', Validators.required],
      obstacle: ['', Validators.required],
      nextDayPlan: ['', Validators.required]
    })
  }

  addMoreTask() {
    const control = this.form.controls['tasks'] as FormArray
    control.push(this.userTaskForm());
  }
  removeTask(i: number) {
    const control = this.form.controls['tasks'] as FormArray
    control.removeAt(i);
  }

  onSave() {
    debugger
    
    if(this.form.controls['tasks'].invalid) return this.form.controls['tasks'].markAllAsTouched();
    console.log(this.service.userId());
    // console.log(this.form.controls['tasks'].value);
    //   for(let i=0;i<this.form.controls['tasks'].value[i];i++){
    //        this.usertasks.push(this.form.controls['tasks'].value[i])
    //   }
    //   this.usertask.TaskName= this.form.controls['tasks'].value[0].taskName;
     //this.usertasks = 
    // this.service.addUserTask(this.form.controls['tasks'].value).subscribe(
    //   (response) => alert('task added successfully'),
    //   (error) => alert('something went wrong')
    //    )
    console.log(this.form.controls['tasks'].value);
    this.service.addUserTask(this.form.controls['tasks'].value).subscribe(
      (response)=>{
        alert('task added successfully');
        $('#newModal').hide();
      },
      (error)=>{
        console.log(error);
        
      }
    )
  }
  get tasks() {
    return this.form.get('tasks') as FormArray;
  }
}