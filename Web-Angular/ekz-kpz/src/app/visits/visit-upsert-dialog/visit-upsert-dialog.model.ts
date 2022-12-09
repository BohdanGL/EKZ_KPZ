import { FormGroup } from '@angular/forms';
import { Patient } from 'src/app/patients/patient.model';

export interface VisitDialogData {
    patients: Patient[];
    form: FormGroup;
    isSubmit: boolean;
  }