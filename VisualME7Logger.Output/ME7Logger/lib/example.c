/*
 * EXAMPLE.C
 *
 * Example for using the library ME7Logger.dll / libME7Logger.so
 *
 * This example opens a connection and logs 50 samples, then terminates.
 * mki, 03/2013
 */

#include <stdio.h>
#include <stdlib.h>
#include "ME7Logger.h"


ME7LoggerHandle     handle;
ME7LoggerDataDesc * data_desc;


/*
 * This is the callback function that gets called from the logger
 * whenever a new result is available.
 */
int my_callback(ME7LoggerHandle handle, const ME7LoggerDataDesc * data_desc, const ME7LoggerDataValues * data_values)
{
    static int result_count = 0;
    int i;

    if (result_count == 0)
    {
        // 1st call of callback: print header line with variable names
        printf("==> ##: Time;");
        for (i = 0; i < data_desc->num_variables; i++)
        {
            printf(" %s;", data_desc->variables[i].name);
        }
        printf("\n");
    }

    result_count++;

    printf("==> %2d: ", result_count);

    // Print timestamp
    if (data_values->absolute_time != NULL)
    {
        printf("%02u:%02u:%02u.%03u;",
               data_values->absolute_time->hour,
               data_values->absolute_time->minute,
               data_values->absolute_time->second,
               data_values->absolute_time->millisecond);
    }
    else
    {
        printf("%lu;", data_values->relative_time);
    }

    // Print all received values
    for (i = 0; i < data_values->num_variables; i++)
    {
        printf(" %g;", data_values->values[i]);
    }
    printf("\n");

    if (result_count >= 50)
    {
        // Stop the logging after 50 results where received
        // Normally this would be controlled by a global flag variable
        return ME7LOGGER_STOP;
    }

    return ME7LOGGER_CONTINUE;
}


int main(int argc, char **argv)
{
    int i;
    char * log_config_file;

    if (argc != 2)
    {
        fprintf(stderr, "Usage: %s <log_config_file>\n", argv[0]);
        exit(1);    
    }
    log_config_file = argv[1];

    // Open ME7Logger
    handle = me7logger_open();
    if (handle == NULL)
    {
        fprintf(stderr, "Could not open me7logger\n");
        exit(1);
    }

    // Create a logfile (only needed for debugging)
    FILE * fp = fopen("me7logger.log", "w");
    me7logger_set_logfile(handle, fp);

    // Configure logging (this reads the trace config file and the ecu characteristics file)
    if (me7logger_config(handle, argv[0], log_config_file) != ME7LOGGER_OK)
    {
        fprintf(stderr, "Could not configure logging\n");
        exit(1);
    }

    // Get result data description for current log configuration
    if ((data_desc = me7logger_get_data_desc(handle)) == NULL)
    {
        fprintf(stderr, "Could not get data description\n");
        exit(1);
    }

    // Print all logged variables and their unit
    printf("==> Logging %d variables:\n", data_desc->num_variables);
    for (i = 0; i < data_desc->num_variables; i++)
    {
        printf("==> %d: %s [%s]\n", i, data_desc->variables[i].name, data_desc->variables[i].unit);
    }

    ME7LoggerConfiguration logger_config;

    // Use serial interface
    //logger_config.itf_type     = ME7LOGGER_ITF_SERIAL;
    //logger_config.itf_instance = "COM2";

    // Use FTDI interface
    logger_config.itf_type     = ME7LOGGER_ITF_FTDI;
    logger_config.itf_instance = NULL;

    logger_config.baudrate            = 0;          // Use baudrate from trace config
    logger_config.samples_per_second  = 0;          // Use sps from trace config
    logger_config.absolute_timestamps = 0;          // 0 = off (timestamps relative to start of logging)
    logger_config.sync_to_full_second = 0;          // 0 = off (don't wait for full second before start of logging)

    // Start logging
    if (me7logger_run(handle, &logger_config, my_callback) != ME7LOGGER_OK)
    {
        fprintf(stderr, "Could not start logging or logging has stopped by error\n");
        exit(1);
    }
    else
    {
        // me7logger_run() will return success AFTER logging was stopped!!
        printf("==> logging has finished successfully\n");
    }

    // Release logger handle
    me7logger_close(handle);

    // Close debug logfile
    fclose(fp);

    return(0);
}


