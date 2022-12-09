import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EntitiesRoutingModule } from './patients-routing.module';
import { PatientsComponent } from './patients.component';
import { PatientsService } from './patients.service';
import { HttpClientModule } from '@angular/common/http';
import { PatientUpsertDialogComponent } from './patient-upsert-dialog/patient-upsert-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { PatientFormService } from './patient-form.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    PatientsComponent,
    PatientUpsertDialogComponent
  ],
  imports: [
    CommonModule,
    EntitiesRoutingModule,
    HttpClientModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  providers: [PatientsService, PatientFormService]
})
export class PatientsModule { }
