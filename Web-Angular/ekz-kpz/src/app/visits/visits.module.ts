import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VisitsRoutingModule } from './visits-routing.module';
import { VisitsComponent } from './visits.component';
import { VisitUpsertDialogComponent } from './visit-upsert-dialog/visit-upsert-dialog.component';
import { HttpClientModule } from '@angular/common/http';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { VisitsService } from './visits.service';
import { VisitFormService } from './visit-form.service';
import { MatSelectModule } from '@angular/material/select';
import { PatientsService } from '../patients/patients.service';


@NgModule({
  declarations: [
    VisitsComponent,
    VisitUpsertDialogComponent
  ],
  imports: [
    CommonModule,
    VisitsRoutingModule,
    HttpClientModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule
  ],
  providers: [VisitsService, VisitFormService, PatientsService]
})
export class VisitsModule { }
